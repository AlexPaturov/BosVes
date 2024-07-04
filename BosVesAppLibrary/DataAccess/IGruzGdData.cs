
namespace BosVesAppLibrary.DataAccess;

public interface IGruzGdData
{
   Task<IEnumerable<GruzGdModel>> GetAll();
   Task<IEnumerable<GruzGdModel>> GetNameByPartName(string name);
   Task InsNew(GruzGdModel gruz);
   Task UpdGruz(GruzGdModel gruz);
}