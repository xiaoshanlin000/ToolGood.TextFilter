#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/layer/default/loading-2.gif")]
        public IActionResult __layui_css_modules_layer_default_loading_2_gif()
        {
            if (SetResponseHeaders("50C5E3E79B276C92DF6CC52CAEB464F0") == false) { return StatusCode(304); }
            const string s = "G/oGIGaItnmPL/wre9LKTcxCGyMSCwNjYRZhRSwblpXQf7t0nxV+aCZtkgLQxBMJhE74gI717M0R4EFebMkJReAX1l14enswbZLUgTrYCsD/Ziu/ucZDPrEHHhZAKHGEPv4SBjg1/ksHuIeFfo88mBmZSEBg0PgJS0qCbADW5v8UgJ+TT3jr+i6/Ojvn42NJHpFVDp8M8SV30yd0sBkG7aGiiaW+8ck1nOwVpV2HiaYKOLzdKklTV2h4A/DWQ/94uaYnsijrIAjWNM8Js9zDvshMmqe0PnARv9u2JYWOFRV7OkRDeC4dEhR5cWpVChGWQ6cJHMEeZtfbwdJEwvTM8JjdzOKQ13zj5NLyyFaVfpMd2/O1OrzyGppyo+29qox4tKMNO3vhA52cg7QYRc7xQHeuLdRf5RkdZyRyuMW6Hxg8nFf7DLxicw6nG9m8ccjjfq0aTW7+nPpIBbmhR227VpsGrN3shzyQU66bJSnL1w30RcLWhm5OiFdn+onIUeqU3uosP4ah7qsJU0MCVti/BrTK6hLk56nT/qeP5s+zzx5+WHsoJKWpgbWk4oVHznRQ1euQ12dFb5vWHavb9Hf+3u/HMB4SCKsAX1hTxWsQ1La1d3R2eZaWUUgEFRWAJa6z0RXKudEif0Gwhs5TnVM6KnXMiX76pq+I2uFhH6TXl3PjZ+6uCSFkDqUDnK/GENlK4rF5QSKPUefGZyHypbQ0v8sxekfQ+MS1uzxITRdxUOAV2by8Ep+KMDqM5wUYJeVE5aW7Yh1RPsoSIRQAhQyT6LVoi9dnsdP38WTXwmEFfj2r2v85a0HzAh5u9mlVzBT0srQlTMJaFUUX9CAO9aqP9y7vcRDjMYEEyfr7g4fKeTIc7tmciFU9Q9+6H9Uo7S78odH3/rclyuNLOVXw69AqUI8KAYQFFch/49TsTdDifqYJkfgUrIw/k1P9MGrFM7Qx9qqiDlvBcPDY2++tUOjyIWf1oCYJ8SMTzVr1xs/zS34q2xa/LrwtPShTU+x3teV8o14MD6neT0j43u61a8/XPbhGtye01HCiVElRTD669rhUa3W7PM74DVggd4CFmJ2JrBG7zzIyJaevN4pcehkPcEWVjVb4JQ5TGT8rzk60yC2HPT5Xf/Zvczx9aOAoce8DA45fYbnTEwuX5ULhfdlTQxNBotcFR2xeivTvMAioGYQUMzAUzZKrZcCNaHgcHy1cJ9VIWAd1ZCONuUlhDK6ZBCBAAWrOy8gnUPyE/EbyFu3hh6l46+Yr5Em6HHv1UUNdQqi1czdj726Lbv3EEyVhvFC/SbatQ918N0Xos3E+q/vXG1sJBD6sgNmNph6pnxhnnko+xZpgBh7LdOYYT+jbqpahtPwSBT4NyV9ZL2yqK6dhbfIthqpS6JoAIprjizQjAAPygMpqn976CFOJ8/vM+MMS9R0t6Y14vT3KdTdish3O0MVXCOaEeqVEURck2+BK4M3LJ7oAzY5sQI9Ct9pwgmjKyy6HEmMcpBY/DfYw+x0VCwwooTtG8jXPzQb2q4SxC9bQymXsZBApAVOtUo9h4wA1Jyq2vCT5+NLDbSqs9jjPTmXVVCGPFcTjDOpu3yTW1Mjjvxg9+lBSfdlF0Oktbif294oeXhgmRsC1qGWODl1HGGXm0GxwxPvNXiUJ4tNbsHjtSkbzAgv7ce9tRI5lvhSUt5ZawtZNUKmrouG5qrUyGNJanttuybMBIKXFJbd9wU1/F/N1bNmtREZJQvroC29l3UUdP2nN9/rcgPXMF+TVlkfea975gWvyE82y1I3qXmg5a40PBzp3SzIVAzCun/5++XkB+fBJQS8Ev8ruLcgzls48sXAzEx+RqUgkJFsrShO8lapDAAGUV2ebTOdX7/8xOkA/rV6djW4Ryk+1p+8IXmMVlrPmfvGG9zoCdSvB4+9LFJ6g9/3zXWRM+8L7vCRlDHJObBqMM7y9+kWpSfVudYmfVNWELu5m6aYMAT63SSPxAIUI+204gAGVR+WRT6A3JqI6u4OOsanll8W6T6azI08JBa+CkefzjfxmUec2qqfR8/oKXL2GnuQJ036A61bIVt9wg1hoNxW2++5Cf5+ZMiZ96cGZf1Hj23SPladNa0E2iRUmQbIQ5KEG81LU8DyBggSvMpVX6gkV6cJNFGAH";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif