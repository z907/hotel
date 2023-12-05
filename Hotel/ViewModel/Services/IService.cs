using System.Net.Sockets;
using ViewModel.VmEntities;

namespace ViewModel.Services;

public interface IService
{
   void Add(IEntity item);
}