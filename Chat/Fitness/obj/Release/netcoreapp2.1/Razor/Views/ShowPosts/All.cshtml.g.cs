#pragma checksum "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "144d457073baca5c73db3378fb3425abcd1adea1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShowPosts_All), @"mvc.1.0.view", @"/Views/ShowPosts/All.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ShowPosts/All.cshtml", typeof(AspNetCore.Views_ShowPosts_All))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"144d457073baca5c73db3378fb3425abcd1adea1", @"/Views/ShowPosts/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f65067f5168b29dd9d623309be4e654a1b4da021", @"/Views/_ViewImports.cshtml")]
    public class Views_ShowPosts_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Post>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/css/datatables.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("border: 1px solid #999;border-radius: 10px;width:100% ;height:70px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/datatables/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/datatables/custom-basic.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
  
    ViewData["Title"] = "كل البوستات";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(117, 32, true);
            WriteLiteral("    <!-- Datatables css-->\r\n    ");
            EndContext();
            BeginContext(149, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "144d457073baca5c73db3378fb3425abcd1adea16285", async() => {
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
            BeginContext(223, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(228, 1051, true);
            WriteLiteral(@"<div class=""col-sm-12"" style=""margin-top:40px"">
    <div class=""card"">
        <div class=""card-header"">
            <h5>البوستات المنشوره</h5>
        </div>
        <div class=""card-body order-datatable"">
            <table class=""display"" id=""basic-1"" style=""display:inline-table"" id=""productTbl"">
                <thead class=""text-center"">
                    <tr>
                        <th style=""width:20%"">
                            <label>اسم المستخدم</label>
                        </th>
                        <th style=""width:35%"">
                            <label>المحتوى</label>
                        </th>
                        <th style=""width:10%"">
                            <label>تاريخ النشر</label>
                        </th>
                        <th style=""width:15%"">
                            <label>المرفقات</label>
                        </th>
                        <th style=""width:20%"">الادوات</th>
                    </tr>
                </thead>");
            WriteLiteral("\r\n                <tbody>\r\n");
            EndContext();
#line 33 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                     foreach (var item in Model.OrderByDescending(a=>a.publishDate))
                    {

#line default
#line hidden
            BeginContext(1388, 96, true);
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
            EndContext();
            BeginContext(1485, 58, false);
#line 37 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                           Write(Html.DisplayFor(modelItem => item.ApplicationUser.FulName));

#line default
#line hidden
            EndContext();
            BeginContext(1543, 71, true);
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n");
            EndContext();
#line 40 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                  
                                    if (!string.IsNullOrEmpty(item.content) && item.content!= "undefined")
                                    {
                                        if (item.content.Length > 100)
                                        {

#line default
#line hidden
            BeginContext(1912, 50, true);
            WriteLiteral("                                            <span>");
            EndContext();
            BeginContext(1963, 29, false);
#line 45 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                             Write(item.content.Substring(0, 99));

#line default
#line hidden
            EndContext();
            BeginContext(1992, 10, true);
            WriteLiteral(" </span>\r\n");
            EndContext();
#line 46 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"

                                        }
                                        else
                                        {

#line default
#line hidden
            BeginContext(2136, 50, true);
            WriteLiteral("                                            <span>");
            EndContext();
            BeginContext(2187, 12, false);
#line 50 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                             Write(item.content);

#line default
#line hidden
            EndContext();
            BeginContext(2199, 9, true);
            WriteLiteral("</span>\r\n");
            EndContext();
#line 51 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                        }
                                    }
                                    else
                                    {

#line default
#line hidden
            BeginContext(2371, 86, true);
            WriteLiteral("                                        <span style=\"color:red\">لا يوجد محتوى</span>\r\n");
            EndContext();
#line 56 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                    }
                                

#line default
#line hidden
            BeginContext(2531, 107, true);
            WriteLiteral("                            </td>\r\n                            <td>\r\n                                <span>");
            EndContext();
            BeginContext(2639, 36, false);
#line 60 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                 Write(item.publishDate.ToShortDateString());

#line default
#line hidden
            EndContext();
            BeginContext(2675, 78, true);
            WriteLiteral("</span>\r\n                            </td>\r\n                            <td>\r\n");
            EndContext();
#line 63 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                 if (!string.IsNullOrEmpty(item.ImgUrl))
                                {

#line default
#line hidden
            BeginContext(2862, 176, true);
            WriteLiteral("                                    <div style=\"background-color: white; padding: 2px;border: 1px solid #d0cfcf;border-radius: 10px;\">\r\n                                        ");
            EndContext();
            BeginContext(3038, 132, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "144d457073baca5c73db3378fb3425abcd1adea113773", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3048, "~/PostsImage/", 3048, 13, true);
#line 66 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
AddHtmlAttributeValue("", 3061, item.ImgUrl, 3061, 12, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3170, 46, true);
            WriteLiteral("\r\n                                    </div>\r\n");
            EndContext();
#line 68 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                                }

#line default
#line hidden
            BeginContext(3251, 143, true);
            WriteLiteral("                            </td>\r\n                            <td>\r\n                                <a class=\"btn btn-success\" target=\"_blank\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 3394, "\"", 3428, 2);
            WriteAttributeValue("", 3401, "/ShowPosts/Details/", 3401, 19, true);
#line 71 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
WriteAttributeValue("", 3420, item.Id, 3420, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3429, 83, true);
            WriteLiteral(">عرض</a>\r\n                                <button class=\"btn btn-primary btnDelete\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 3512, "\"", 3525, 1);
#line 72 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
WriteAttributeValue("", 3517, item.Id, 3517, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3526, 81, true);
            WriteLiteral(">حذف</button>\r\n                            </td>\r\n                        </tr>\r\n");
            EndContext();
#line 75 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\ShowPosts\All.cshtml"
                    }

#line default
#line hidden
            BeginContext(3630, 86, true);
            WriteLiteral("                </tbody>\r\n            </table>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(3733, 32, true);
                WriteLiteral("\r\n    <!-- Datatable js-->\r\n    ");
                EndContext();
                BeginContext(3765, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "144d457073baca5c73db3378fb3425abcd1adea117786", async() => {
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
                BeginContext(3836, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(3842, 62, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "144d457073baca5c73db3378fb3425abcd1adea119042", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3904, 1156, true);
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $('#productTbl').DataTable()
        });
        $("".btnDelete"").click(function () {
            var btn = this;
            $(btn).css(""display"", ""none"")
            var Id = btn.id;
            if (Id > 0) {
                if (confirm(""هل انت متاكد ؟"")) {
                    //Ajax Code Start
                    $.ajax({
                        url: ""/ShowPosts/Delete?postId="" + Id,
                        type: ""POST"",
                        success: function (data) {
                            if (data.msg == 'Done') {
                                var clickedRow = btn.parentElement.parentElement;
                                clickedRow.remove();
                                alert(""تك الحذف بنجاح"")
                            } else {
                                alert(""يرجى المحاوله مره اخرى"")
                                $(btn).css(""display"", ""block"")
                            }
                 ");
                WriteLiteral("       }\r\n                    })\r\n                    //Ajax Code End\r\n                }\r\n            }\r\n        })\r\n    </script>\r\n");
                EndContext();
            }
            );
            BeginContext(5063, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Post>> Html { get; private set; }
    }
}
#pragma warning restore 1591