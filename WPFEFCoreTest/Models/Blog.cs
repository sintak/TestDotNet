using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEFCoreTest.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }
}
