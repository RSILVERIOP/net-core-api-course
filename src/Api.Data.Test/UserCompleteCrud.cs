using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entites;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UserCompleteCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UserCompleteCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }        

        [Fact(DisplayName = "User Crud")]
        [Trait("CRUD", "UserEntity")]
        public async Task Is_Possible_Do_User_Crud()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _createdRecord = await _repository.InsertAsync(_entity);
                
                Assert.NotNull(_createdRecord);
                Assert.Equal(_entity.Email, _createdRecord.Email);
                Assert.Equal(_entity.Name, _createdRecord.Name);
                Assert.False(_createdRecord.Id  == Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _updatedRecord = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_updatedRecord);
                Assert.Equal(_entity.Email, _updatedRecord.Email);
                Assert.Equal(_entity.Name, _updatedRecord.Name);

                var _existsRecord = await _repository.ExistAsync(_updatedRecord.Id);
                Assert.True(_existsRecord);

                var _selectRecord = await _repository.SelectAsync(_updatedRecord.Id);
                Assert.NotNull(_selectRecord);
                Assert.Equal(_updatedRecord.Email, _selectRecord.Email);
                Assert.Equal(_updatedRecord.Name, _selectRecord.Name);

                var _allRecords = await _repository.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() > 1);

                var _deleted = await _repository.DeleteAsync(_selectRecord.Id);
                Assert.True(_deleted);

                var _adminUser = await _repository.FindByLogin("Admin@gmail.com");
                Assert.NotNull(_adminUser);
                Assert.Equal("Admin@gmail.com", _adminUser.Email);
                Assert.Equal("Admin", _adminUser.Name);
            }
        }
    }
}