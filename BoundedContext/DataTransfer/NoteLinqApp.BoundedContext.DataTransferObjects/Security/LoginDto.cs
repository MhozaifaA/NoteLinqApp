using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorLinqApp.DataTransferObjects.Security
{
    public class LoginDto
    {
        /// <summary>
        /// default:user
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// default:user
        /// </summary>
        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
