using System.Collections.Generic;
using WinForms_DAO_DTO_Singleton.DataTransfer;
using WinForms_DAO_DTO_Singleton.Entities;

namespace WinForms_DAO_DTO_Singleton.Contracts
{
    public interface ICustomerDAO
    {
        int Create(Customer customer);
        int Edit(Customer customer);
        int Remove(Customer customer);
        Customer GetById(int id);
        List<CustomerDTO> GetAll();
        List<Customer> Search(string name);
    }
}
