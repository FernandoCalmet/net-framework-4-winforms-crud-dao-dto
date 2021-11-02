using System.Collections.Generic;
using WinForms_DAO_DTO_Singleton.DataTransfer;
using WinForms_DAO_DTO_Singleton.Entities;

namespace WinForms_DAO_DTO_Singleton.Contracts
{
    public interface ICustomerDAO
    {
        int Create(Customer customer);
        int Edit(Customer customer);
        int Remove(int id);
        Customer GetById(int id);
        CustomerDTO GetDTOById(int id);
        List<Customer> GetAll();
        List<Customer> Search(string name);
    }
}
