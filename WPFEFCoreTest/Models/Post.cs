﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEFCoreTest.Models
{
    public class Post
    {
         public int ID { get; set; }
         public string Title { get; set; }
         public string Content { get; set; }
 
         public int BlogId { get; set; }
         public Blog Blog { get; set; }
    }
}
