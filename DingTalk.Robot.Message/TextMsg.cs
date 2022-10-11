using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DingTalk.Robot
{
    public class TextMsg
    {
        public readonly string Msgtype = "text";
        public TextContent? Text { get; set; }
    }
    public class TextContent
    {
        public string? Content { get; set; }
    }
}
