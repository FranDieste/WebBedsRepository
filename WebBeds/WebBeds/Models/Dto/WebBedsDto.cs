using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBeds.UI.Models.Dto
{
    public class WebBedsDto
    {
        public IEnumerable<WebBedsParent> WebBedsParent { get; set; }
    }

    public class WebBedsParent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<WebBedsChield> WebBedsChildren { get; set; }
    }

    public class WebBedsChield
    {
        public string ChieldCode { get; set; }
        public string ChieldName { get; set; }
        public string ChieldAddress { get; set; }
        public string PhoneEmail { get; set; }

    }
}
