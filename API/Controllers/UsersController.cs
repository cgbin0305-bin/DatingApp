using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
  When HTTP Req comes in => if it has a address  ../api/users it will routed to this controller 
  => framework going to see user's controller and see the constructor => create a new instance of the data context then inject to controller
  => then controller work => send out the http res => DBcontext disposed
*/
namespace API.Controllers;
/*
  We want to send a req from Angular to hit this controller and come to endpoint to get a list of users or get user by id => display them inside our browser
*/
[Authorize] // when user is authorized => user be allowed access to endpoint 
/*
Authorize can be set on controller-level => AllowAnonymous can be set in side controller 
=> AllowAnonymous put at the controller-level => Authorize can not be put in side controller
*/
public class UsersController : BaseApiController
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  // the rest of block code below could be use it
  public UsersController(IUserRepository userRepository, IMapper mapper) // access to our DB => query users then return them from our API controller
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }
  // API endpoint 
  // [AllowAnonymous]
  [HttpGet] // Get /api/users
  public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers()
  {
    var users = await _userRepository.GetMembersAsync();
    return Ok(users);
  }
  [HttpGet("{username}")]
  public async Task<ActionResult<MembersDto>> GetUSer(string username)
  {
    return await _userRepository.GetMemberAsync(username);
  }

}
