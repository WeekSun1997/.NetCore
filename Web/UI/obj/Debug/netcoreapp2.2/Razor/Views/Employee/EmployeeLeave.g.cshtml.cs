#pragma checksum "G:\Program Files (x86)\Web\Web\UI\Views\Employee\EmployeeLeave.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_EmployeeLeave), @"mvc.1.0.view", @"/Views/Employee/EmployeeLeave.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employee/EmployeeLeave.cshtml", typeof(AspNetCore.Views_Employee_EmployeeLeave))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "G:\Program Files (x86)\Web\Web\UI\Views\_ViewImports.cshtml"
using UI;

#line default
#line hidden
#line 2 "G:\Program Files (x86)\Web\Web\UI\Views\_ViewImports.cshtml"
using UI.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a", @"/Views/Employee/EmployeeLeave.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52d79ad08d11418ded2b13adb4a9b2619d15bb23", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_EmployeeLeave : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/layuiadmin/layui/css/layui.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/layuiadmin/layui/layui.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "G:\Program Files (x86)\Web\Web\UI\Views\Employee\EmployeeLeave.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(29, 29, true);
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            EndContext();
            BeginContext(58, 4452, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a4837", async() => {
                BeginContext(64, 96, true);
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>EmployeeHt</title>\r\n    ");
                EndContext();
                BeginContext(160, 65, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a5320", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(225, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(231, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a6652", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(282, 4221, true);
                WriteLiteral(@"
    <script>
        layui.use(['form', 'table'], function () {
            var table = layui.table;
            var $ = layui.jquery;
            table.render({
                elem: '#tabs'
                , height: 500
                , url: 'http://127.0.0.1:5001/api/Employee/EmployeeLeave' //数据接口
                , page: true //开启分页
                , toolbar: '#toolbarDemo'
                , cols: [[
                    { field: 'userName', title: '姓名', sort: true, fixed: 'left' },
                    { field: 'leaveDate', title: '请假日期', sort: true, fixed: 'left' },
                    {
                        field: 'leaveType', title: '是否调休', sort: true, fixed: 'left', templet: function (d) {
                            var online = """";
                            if (d.employeeType == 1) {
                                online = ""checked='true'""
                            }
                            return '<input type=""checkbox"" name=""employeeType"" lay-skin=""switch"" lay-text=");
                WriteLiteral(@"""调休|请假"" ' + online + ''
                        }
                    },
                    {
                        field: 'leaveReasons', title: '请假事由', sort: true, fixed: 'left',templet: function(d) {
                            var statc = """";
                            console.info(d.leaveReasons)
                            switch (d.leaveReasons) {
                                case 1:
                                    statc = ""病假""
                                    break;
                                case 2:
                                    statc = ""事假"";
                                    break;
                                case 3:
                                    statc = ""其他"";
                                    break;
                                case 4:
                                    statc = ""调休"";
                                    break;
                                default:
                                    statc = 0;
                      ");
                WriteLiteral(@"              break;
                                   
                            }
                            return '<a>' + statc + '</a>'
                        }
                    },
                    { field: 'Overtime', title: '调休日期', sort: true, fixed: 'left' },
                    { fixed: 'billId', title: '操作', toolbar: '#barDemo', width: 150 }
                ]]
                , parseData: function (res) { //res 即为原始返回的数据
                    console.info(res.data)
                    return {
                        ""code"": res.isSuccess == true ? 0 : 500, //解析接口状态
                        ""msg"": res.msg, //解析提示文本
                        ""data"": res.data //
                        
                    };
                }
            })
            $(""#AddEmployee"").click(function () {
                layer.open({
                    type: 2 //此处以iframe举例
                    , title: '创建新表'
                    , area: ['400px', '600px']
                    , shade: 0");
                WriteLiteral(@"
                    , maxmin: true
                    , content: '/Employee/InsertLeave'
                })

            })
            table.on('tool(tabs)', function (obj) {
                var data = obj.data;
                //console.log(obj)
                console.info(obj)
                if (obj.event === 'del') {
                    layer.confirm('真的删除行么', function (index) {
                        $.ajax({
                            url: ""http://127.0.0.1:5001/api/Employee/DelLeave"",
                            data: { BillID: obj.data.billId },
                            xhrFields: { withCredentials: true },
                            type: 'post',
                            success: function (data) {
                                console.info(data)
                                if (data.isSuccess == true) {
                                    obj.del();
                                    layer.close(index);
                                }
                      ");
                WriteLiteral("      }\r\n                        })\r\n                    });\r\n                } \r\n            })\r\n        })\r\n    </script>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4510, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(4512, 436, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1e8f1d6a001344f39d04c3b3fd8525ad5a1ea04a13113", async() => {
                BeginContext(4518, 423, true);
                WriteLiteral(@"
    <script type=""text/html"" id=""toolbarDemo"">
        <div class=""layui-btn-container"">
            <button class=""layui-btn layui-btn-sm"" id=""AddEmployee"" lay-event=""getCheckData"">新增请假</button>
        </div>
    </script>
    <table id=""tabs"" lay-filter=""tabs""></table>
    <script type=""text/html"" id=""barDemo"">
        <a class=""layui-btn layui-btn-danger layui-btn-xs"" lay-event=""del"">删除</a>
    </script>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4948, 11, true);
            WriteLiteral("\r\n</html>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
