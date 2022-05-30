using Api.Domain.Dtos.User;
using Api.Domain.Entites;
using Api.Domain.Models;
using Api.Services.Test;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        [Fact(DisplayName = " Is Possible Map the models")]
        public void Is_Possible_Mapp_the_Models()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var entityList  = new List<UserEntity>();
            for (int i = 0; i < 5; i ++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                entityList.Add(item);
            }

            // Model to Entity
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity to Dto
            var userDto = Mapper.Map<UserDto>(entity);
            Assert.Equal(userDto.Id, entity.Id);
            Assert.Equal(userDto.Name, entity.Name);
            Assert.Equal(userDto.Email, entity.Email);
            Assert.Equal(userDto.CreateAt, entity.CreateAt);


            var dtoList = Mapper.Map<List<UserDto>>(entityList);
            Assert.True(dtoList.Count() == entityList.Count());
            for (int i = 0; i < dtoList.Count(); i ++)
            {
                Assert.Equal(dtoList[i].Id, entityList[i].Id);
                Assert.Equal(dtoList[i].Name, entityList[i].Name);
                Assert.Equal(dtoList[i].Email, entityList[i].Email);
                Assert.Equal(dtoList[i].CreateAt, entityList[i].CreateAt);
            }

            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
            Assert.Equal(userDtoCreateResult.Id, entity.Id);
            Assert.Equal(userDtoCreateResult.Name, entity.Name);
            Assert.Equal(userDtoCreateResult.Email, entity.Email);
            Assert.Equal(userDtoCreateResult.CreateAt, entity.CreateAt);

            var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(entity);
            Assert.Equal(userDtoUpdateResult.Id, entity.Id);
            Assert.Equal(userDtoUpdateResult.Name, entity.Name);
            Assert.Equal(userDtoUpdateResult.Email, entity.Email);
            Assert.Equal(userDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto to Model
            var UserModel = Mapper.Map<UserModel>(userDto);
            Assert.Equal(UserModel.Id, userDto.Id);
            Assert.Equal(UserModel.Name, userDto.Name);
            Assert.Equal(UserModel.Email, userDto.Email);
            Assert.Equal(UserModel.CreateAt, userDto.CreateAt);

            var userDtoCreate = Mapper.Map<UserDtoCreate>(UserModel);
            Assert.Equal(userDtoCreate.Name, UserModel.Name);
            Assert.Equal(userDtoCreate.Name, UserModel.Name);

            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(UserModel);
            Assert.Equal(userDtoUpdate.Id, UserModel.Id);
            Assert.Equal(userDtoUpdate.Name, UserModel.Name);
            Assert.Equal(userDtoUpdate.Name, UserModel.Name);
        }
    }
}