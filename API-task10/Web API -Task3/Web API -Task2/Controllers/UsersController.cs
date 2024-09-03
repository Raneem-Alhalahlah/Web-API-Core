﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Web_API__Task2.DTOs;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        private readonly TokenGenerator _tokenGenerator;
        public UsersController(MyDbContext myDbContext ,TokenGenerator tokenGenerator)
        {
            _myDbContext = myDbContext;
            _tokenGenerator = tokenGenerator;
        }


        //[HttpGet]
        //[Route("getAllUsers")]
        //public IActionResult GetAllUsers()
        //{
        //    var users = _myDbContext.Users.ToList();
        //    return Ok(users);
        //}


        //[HttpGet]
        //[Route("GetUserById/{id:int:min(10)}")]
        //public IActionResult GetUserById(int id)
        //{


        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}


        //[HttpGet]
        //[Route("GetUserByName/{name}")]
        //public IActionResult GetUserByName(string name)
        //{
        //    var user = _myDbContext.Users
        //       .FirstOrDefault(c => c.Username == name);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}


        //[HttpDelete]
        //[Route("DeleteUser/{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _myDbContext.Users.Remove(user);
        //    _myDbContext.SaveChanges();

        //    return NoContent();
        //}


        [HttpPost("Regester")]
        public IActionResult addUser([FromForm] UserDTO userDTO)
        {
            byte[] passwordHash;
            byte[] salt;

            PasswordHasherNew.createPasswordHash(userDTO.Password, out passwordHash, out salt);

            var user = new User
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                Username = userDTO.UserName
            };


            _myDbContext.Users.Add(user);
            _myDbContext.SaveChanges();
            return Ok(user);
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            var user = _myDbContext.Users.FirstOrDefault(x => x.Username == userDTO.UserName);
            if (user == null || !PasswordHasherNew.VerifyPasswordHash(userDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }
            //عشان jwt
            var roles = _myDbContext.UserRoles.Where(x => x.UserId == user.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);

            return Ok(new { Token = token });

            //return Ok("User logged in successfully");
        }




        [HttpGet]
        [Route("GetUserById/{id:int}")]
        public IActionResult GetUserById(int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var user = _myDbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }


        //problem solving

        [HttpPost("FindSingleRepetition")]
        public IActionResult FindSingleRepetition([FromBody] List<int> numbers)
        {

            var singleRepetitions = numbers.GroupBy(n => n)
                .Where(g => g.Count() % 2 == 1)
                .Select(g => g.Key)
                .ToList();
            return Ok(singleRepetitions);

        }



      
    




    }
}












  
