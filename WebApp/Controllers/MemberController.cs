using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
namespace WebApp.Controllers
{
    [Authorize(Roles= "Administrator")]
    public class MemberController : Controller
    {
        MemberRepository repository;
        RoleRepository roleRepository;
        MemberInRoleRepository memberInRoleRepository;
        public MemberController(IConfiguration configuration)
        {
            repository = new MemberRepository(configuration);
            roleRepository = new RoleRepository(configuration);
            memberInRoleRepository = new MemberInRoleRepository(configuration);
        }
        public IActionResult Index()
        {
            return View(repository.GetMenbers());
        }
        public IActionResult Roles(Guid id)
        {
            Member obj = repository.GetMemberByID(id);
                //chua dung
                obj.Roles = roleRepository.GetRolesByMember(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Add(MemberInRole obj)
        {
            return Json(memberInRoleRepository.Add(obj));
        }
    }
}
