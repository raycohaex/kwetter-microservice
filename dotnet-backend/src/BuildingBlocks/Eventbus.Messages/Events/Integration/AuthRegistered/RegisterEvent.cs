﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events.Integration.AuthRegistered
{
    public class RegisterEvent
    {
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}
