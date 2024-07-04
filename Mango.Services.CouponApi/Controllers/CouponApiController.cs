using AutoMapper;
using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController : ControllerBase
{
    private readonly AppDbContext _appDb;
    private readonly IMapper _mapper;
    private ResponseDto _response;
    public CouponApiController(AppDbContext appDb, IMapper mapper)
    {
        _appDb = appDb;
        _mapper = mapper;
        _response = new ResponseDto();       
    }
    [HttpGet]
    public ResponseDto GetAllCoupon()
    {
        try
        {
            IEnumerable<Coupon> coupons = _appDb.Coupons.ToList();  
           _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message; 
        }
        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto GetSingleCoupon(int id)
    {
        try
        {
            Coupon coupon = _appDb.Coupons.First(c => c.CouponId == id);
           _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }

    [HttpGet]
    [Route("GetByCode{code}")]
    public ResponseDto GetSingleCouponByCode(string code)
    {
        try
        {
            Coupon coupon = _appDb.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
            if (coupon == null)
                _response.IsSuccess = false;
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }

    [HttpPost]
    public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto); 
            _appDb.Coupons.Add(coupon);
            _appDb.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }

    [HttpDelete]
    public ResponseDto DeleteCoupon(int id)
    {
        try
        {
            Coupon coupon = _appDb.Coupons.First(c => c.CouponId == id);
            _appDb.Coupons.Remove(coupon);
            _appDb.SaveChanges();
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }
    [HttpPut]
    public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _appDb.Coupons.Update(coupon);
            _appDb.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }

}
