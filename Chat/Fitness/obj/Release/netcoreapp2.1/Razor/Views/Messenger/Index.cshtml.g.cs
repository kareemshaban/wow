#pragma checksum "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b67aac4ddddb92f728b1ece888724711df2031a7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Messenger_Index), @"mvc.1.0.view", @"/Views/Messenger/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Messenger/Index.cshtml", typeof(AspNetCore.Views_Messenger_Index))]
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
#line 1 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\_ViewImports.cshtml"
using Fitness;

#line default
#line hidden
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\_ViewImports.cshtml"
using Fitness.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b67aac4ddddb92f728b1ece888724711df2031a7", @"/Views/Messenger/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f65067f5168b29dd9d623309be4e654a1b4da021", @"/Views/_ViewImports.cshtml")]
    public class Views_Messenger_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Fitness.Controllers.customMsg>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/css/datatables.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/datatables/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/datatables/custom-basic.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/notify.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
  
    ViewData["Title"] = "الرسائل";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(131, 32, true);
            WriteLiteral("    <!-- Datatables css-->\r\n    ");
            EndContext();
            BeginContext(163, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "b67aac4ddddb92f728b1ece888724711df2031a75923", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(237, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(242, 721, true);
            WriteLiteral(@"<div class=""col-sm-12"" style=""margin-top:40px"">
    <div class=""card"">
        <div class=""card-header"">
            <h5>الرسائل</h5>
        </div>
        <div class=""card-body order-datatable"">
            <table class=""display"" id=""basic-1"" style=""display:inline-table"">
                <thead>
                    <tr>
                        <th style=""width:15%;text-align:center"">المستخدم</th>
                        <th style=""width:60%;text-align:center"">الرساله</th>
                        <th style=""width:10%;text-align:center"">التاريخ</th>
                        <th style=""width:15%;text-align:center"">الرد</th>
                    </tr>
                </thead>
                <tbody>
");
            EndContext();
#line 24 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
                     foreach (Fitness.Controllers.customMsg item in Model)
                    {

#line default
#line hidden
            BeginContext(1062, 62, true);
            WriteLiteral("                        <tr>\r\n                            <td>");
            EndContext();
            BeginContext(1125, 13, false);
#line 27 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
                           Write(item.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(1138, 66, true);
            WriteLiteral("</td>\r\n                            <td style=\"text-align:center\"> ");
            EndContext();
            BeginContext(1205, 12, false);
#line 28 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
                                                      Write(item.LastMsg);

#line default
#line hidden
            EndContext();
            BeginContext(1217, 65, true);
            WriteLiteral("</td>\r\n                            <td style=\"text-align:center\">");
            EndContext();
            BeginContext(1283, 29, false);
#line 29 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
                                                     Write(item.Date.ToShortDateString());

#line default
#line hidden
            EndContext();
            BeginContext(1312, 101, true);
            WriteLiteral("</td>\r\n                            <td>\r\n                                <a class=\"btn btn-secondary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1413, "\"", 1458, 2);
            WriteAttributeValue("", 1420, "/Messenger/chating?userId=", 1420, 26, true);
#line 31 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
WriteAttributeValue("", 1446, item.UserId, 1446, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1459, 77, true);
            WriteLiteral(">الرد</a>\r\n                            </td>\r\n                        </tr>\r\n");
            EndContext();
#line 34 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Messenger\Index.cshtml"
                    }

#line default
#line hidden
            BeginContext(1559, 84, true);
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(1660, 32, true);
                WriteLiteral("\r\n    <!-- Datatable js-->\r\n    ");
                EndContext();
                BeginContext(1692, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b67aac4ddddb92f728b1ece888724711df2031a711231", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1763, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1769, 62, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b67aac4ddddb92f728b1ece888724711df2031a712487", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1831, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1837, 49, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b67aac4ddddb92f728b1ece888724711df2031a713743", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1886, 3089, true);
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $(""#btnSaveAddRole"").click(function () {
                debugger;
                var roleName = $(""#roleName"").val();
                var _userName = $(""#userNameData"").val();
                if (roleName != """") {
                    $(""#roleName"").css(""border"", ""1px solid #cccccc"")
                } else { $(""#roleName"").css(""border"", ""1px solid red""); $(""#roleName"").select(); return; }
                $.ajax({
                    url: ""/Adminstration/AddRole"",
                    type: ""POST"",
                    data: { ""roleName"": roleName, ""UserName"": _userName },
                    success: function (data) {
                        if (data.msg == 'Done') {
                            $.notify(""تمت اضافه الصلاحيه للمستخدم بنجاح"", {
                                globalPosition: ""top center"",
                                className: ""success""
                            })
                            location.relo");
                WriteLiteral(@"ad();
                        } else {
                            $.notify(data.alertMsg, {
                                globalPosition: ""top center"",
                                className: ""error""
                            })
                        }
                    }
                })
            })

            var addroles = document.getElementsByClassName(""addroles"")
            for (var e = 0; e < addroles.length; e++) {
                var addrole = addroles[e];
                addrole.addEventListener('click', function (event) {
                    var btn = event.target;
                    var userName = btn.id;
                    if (userName != """") {
                        $(""#userNameData"").val(userName);
                        $(""#exampleModal"").modal('show');
                    }
                })
            }
            var CheckBtns = document.getElementsByClassName(""blockUser"")
            for (var e = 0; e < CheckBtns.length; e++) {
          ");
                WriteLiteral(@"      var CheckBtn = CheckBtns[e];
                CheckBtn.addEventListener('click', function (event) {
                    var btn = event.target;
                    var userName = btn.id;
                    var clickedRow = btn.parentElement.parentElement;
                    $.ajax({
                        url: ""/Adminstration/block"",
                        type: ""POST"",
                        data: { ""UserName"": userName },
                        success: function (data) {
                            if (data.msg == 'Done') {
                                $.notify("" تمت حظر المستخدم بنجاح "", { globalPosition: ""top center"", className: ""success"" })
                                $(clickedRow).remove();
                            } else {
                                $.notify(""يرجى المحاوله مره اخرى"", { globalPosition: ""top center"", className: ""error"" })
                            }
                        }
                    })
                })
            }
        })");
                WriteLiteral("\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Fitness.Controllers.customMsg>> Html { get; private set; }
    }
}
#pragma warning restore 1591