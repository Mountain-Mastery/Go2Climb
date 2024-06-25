using Agency.API.Database;
using Agency.API.Domain.Repository;
using Agency.API.Domain.Services;
using Agency.API.Domain.Services.Communication;
using Agency.API.Resources;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Agency.API.Services;

public class AgencyService : IAgencyService
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public AgencyService(IAgencyRepository agencyRepository, IMapper mapper, ApplicationDbContext context)
    {
        _agencyRepository = agencyRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Agency>> ListAsync()
    {
        return await _agencyRepository.ListAsync();
    }

    public async Task<IEnumerable<Domain.Models.Agency>> ListByName(string name)
    {
        return await _agencyRepository.ListByName(name);
    }

    public async Task<Domain.Models.Agency> GetByIdAsync(int id)
    {
        var agency = await _agencyRepository.FindByIdAsync(id);
        if (agency == null) throw new KeyNotFoundException("Agency not found.");
        return agency;
    }

    public async Task RegisterAsync(SaveAgencyResource request)
    {
        //Validate
        if (_agencyRepository.ExistsByEmail(request.Email))
            throw new Exception($"Email {request.Email} is already taken.");

        //Map request to customer
        var customer = _mapper.Map<Domain.Models.Agency>(request);

        //Hash Password
        customer.PasswordHash = BCryptNet.HashPassword(request.Password);

        //Save customer
        try
        {
            await _agencyRepository.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while saving the agency: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateAgencyRequest request)
    {
        var agency = GetById(id);

        //Validate
        if (_agencyRepository.ExistsByEmail(request.Email))
            throw new Exception($"Email {request.Email} is already taken.");

        //Hash Password if entered
        if (!string.IsNullOrEmpty(request.Password))
            agency.PasswordHash = BCryptNet.HashPassword(request.Password);

        //Map request to Customer
        _mapper.Map(request, agency);

        try
        {
            _agencyRepository.Update(agency);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while updating the agency: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var agency = GetById(id);

        try
        {
            _agencyRepository.Remove(agency);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while deleting the agency: {e.Message}");
        }
    }

    public async Task<AgencyResponse> FindById(int id)
    {
        var existingAgency = await _agencyRepository.FindByIdAsync(id);

        if (existingAgency == null)
            return new AgencyResponse("Agency not found.");

        return new AgencyResponse(existingAgency);
    }

    //Helper method
    private Domain.Models.Agency GetById(int id)
    {
        var agency = _agencyRepository.FindById(id);
        if (agency == null) throw new KeyNotFoundException("Agency not found.");
        return agency;
    }
}
