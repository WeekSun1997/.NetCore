using EntityFramework.Core.Models;
using Microsoft.AspNetCore.Http;
using MysqlEntity.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketMagger
{
    public class WebSocketMiddlerware
    {


        private readonly RequestDelegate _next;
        private WebSocketBase _wSConnectionManager { get; set; }
        private WebSocketHandler _wsHanlder { get; set; }
        webdevContext db = new webdevContext();
        public WebSocketMiddlerware(
            RequestDelegate next,
            WebSocketBase wSConnectionManager,
            WebSocketHandler wsHandler)
        {
            _next = next;
            _wSConnectionManager = wSConnectionManager;
            _wsHanlder = wsHandler;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.WebSockets.IsWebSocketRequest)
            {
                var cancellationToken = httpContext.RequestAborted;
                var currentWebSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
                _wSConnectionManager.AddSocket(currentWebSocket, httpContext);
                httpContext.Session.TryGetValue("user", out byte[] user);
                var sysuser = Library.Other.SerializeToObject<Sysuser>(user);
                var SenderID = sysuser.UserId;
                var SenderName = "";
                try
                {
                    var list = db.Message.Where(a => a.IsSuccess == 0 && a.ReceiveUserId == SenderID).ToList();
                    if (list != null && list.Count > 0)
                    {
                        _wSConnectionManager.GetAll().TryGetValue(SenderID.ToString(), out WebSocket web);
                        foreach (var item in list)
                        {
                            item.IsSuccess = 1;
                            db.Message.Update(item);
                            db.SaveChanges();
                            SenderName = db.Sysuser.Where(a => a.UserId == item.SendUserId).Select(a => a.UserName).FirstOrDefault();
                            string msgs = SenderName + "id_]]" + item.ReceiveUserId + "id_]]" + item.SendUserId + "id_]]" + item.MsgBody;
                            await _wsHanlder.SendMessageAsync(web, msgs, cancellationToken);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    var response = await _wsHanlder.RecieveAsync(currentWebSocket, cancellationToken);

                    if (string.IsNullOrEmpty(response) && currentWebSocket.State != WebSocketState.Open) break;
                    WebSocketMsgType WebMsg = JsonConvert.DeserializeObject<WebSocketMsgType>(response);
                    string Errmsg = "";
                    httpContext.Session.TryGetValue("user", out user);
                    sysuser = Library.Other.SerializeToObject<Sysuser>(user);
                    SenderID = sysuser.UserId;
                    SenderName = sysuser.UserName;
                    var mes = new MysqlEntity.Core.Model.Message()
                    {
                        MsgBody = WebMsg.Content,
                        ReceiveUserId = int.Parse(WebMsg.ReceiverID),
                        SendUserId = SenderID,//
                        SendTime = DateTime.Now,
                        IsSuccess = 1
                    };
                    if (!_wSConnectionManager.GetAll().ContainsKey(WebMsg.ReceiverID))
                    {
                        mes.IsSuccess = 0;
                    }
                    else
                    {
                        _wSConnectionManager.GetAll().TryGetValue(WebMsg.ReceiverID, out WebSocket web);
                        if (web.State == WebSocketState.Closed)
                        {
                            mes.IsSuccess = 0;
                        }
                    }
                    foreach (var item in _wSConnectionManager.GetAll())
                    {
                        if (item.Value.State == WebSocketState.Open)
                        {//
                            if (WebMsg.ReceiverID.ToLower() == item.Key.ToLower() || SenderID.ToString().ToLower() == item.Key.ToLower())
                            {
                                try
                                {
                                    var ms = db.Message.Where(a => a.MsgId == mes.MsgId).FirstOrDefault();
                                    if (ms == null)
                                    {
                                        db.Message.Add(mes);
                                        db.SaveChanges();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Errmsg = SenderID + "id_]]" + ex.Message;
                                }
                                Errmsg = string.IsNullOrEmpty(Errmsg) ? SenderName + "id_]]" + WebMsg.ReceiverID + "id_]]" + SenderID + "id_]]" + WebMsg.Content : Errmsg;
                                await _wsHanlder.SendMessageAsync(item.Value, Errmsg, cancellationToken);
                            }
                            continue;
                        }
                        continue;
                    }
                }

                await _wSConnectionManager.RemoveSocket(currentWebSocket);
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
