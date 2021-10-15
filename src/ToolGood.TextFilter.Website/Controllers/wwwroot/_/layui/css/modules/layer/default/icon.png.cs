#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/layer/default/icon.png")]
        public IActionResult __layui_css_modules_layer_default_icon_png()
        {
            if (SetResponseHeaders("551539F873D9EBE0792B120A9867D399") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif