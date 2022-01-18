﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Authorization
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
    }
    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
    }
}
