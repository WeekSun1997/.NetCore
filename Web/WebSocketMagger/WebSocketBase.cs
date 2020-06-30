using EntityFramework.Core.Models;
using Microsoft.AspNetCore.Http;
using MysqlEntity.Core.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
namespace WebSocketMagger
{
    public class WebSocketBase
    {
        private static ConcurrentDictionary<string, WebSocket> _socketConcurrentDictionary = new ConcurrentDictionary<string, WebSocket>();

        public void AddSocket(WebSocket socket, HttpContext context)
        {

            if (!_socketConcurrentDictionary.Values.Contains(socket))
            {
                context.Session.TryGetValue("user", out Byte[] us);
                string socketId = Library.Other.SerializeToObject<Sysuser>(us).UserId.ToString();
                _socketConcurrentDictionary.TryAdd(CreateGuid(socketId), socket);
            }
            else
            {

            }

        }
        public async Task RemoveSocket(WebSocket socket)
        {
            _socketConcurrentDictionary.TryRemove(GetSocketId(socket), out WebSocket aSocket);

            await aSocket.CloseAsync(
                closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Close by User",
                cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }


        public string GetSocketId(WebSocket socket)
        {
            return _socketConcurrentDictionary.FirstOrDefault(k => k.Value == socket).Key;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _socketConcurrentDictionary;
        }

        public string CreateGuid(string socketId)
        {
            using (webdevContext context = new webdevContext())
            {
                string userId = "";
                var user = context.Sysuser.Where(a => a.UserId.ToString() == socketId).FirstOrDefault();
                if (user == null)
                    userId = Guid.NewGuid().ToString();
                else
                    userId = user.UserId.ToString();
                return userId;

            }
        }
    }
}
