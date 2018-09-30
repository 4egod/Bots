using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Messaging
{
    public interface IAttachment
    {
        AttachmentTypes AttachmentType { get; }
    }
}
