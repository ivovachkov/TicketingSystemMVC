﻿using Exam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData db;

        public BaseController() : this(new UowData())
        { }

        public BaseController(IUowData data)
        {
            this.db = data;
        }
    }
}