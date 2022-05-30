using  Api.Domain.Dtos.User;

namespace Api.Services.Test.User
{
    public class UserTest
    {
        public static string? NameUser { get; set; }
        public static string? EmailUser { get; set; }
        public static string? NameUserUpdated { get; set; }
        public static string? EmailUserUpdated { get; set; }

        public static Guid IdUser {get; set;}

        public List<UserDto> listUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserDtoCreate userDtoCreate;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult userDtoUpdateResult;

        public UserTest()
        {
            IdUser = Guid.NewGuid();
            NameUser = Faker.Name.FullName();
            EmailUser = Faker.Internet.Email();
            NameUserUpdated = Faker.Name.FullName();
            EmailUserUpdated = Faker.Internet.Email();

            for(int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                listUserDto.Add(dto);
            }

            userDto = new UserDto()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser
            };

            userDtoCreate = new UserDtoCreate()
            {
                Name = NameUser,
                Email = EmailUser
            };

            userDtoCreateResult = new UserDtoCreateResult()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdate = new UserDtoUpdate()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser
            };

            userDtoUpdateResult = new UserDtoUpdateResult()
            {
                Id = IdUser,
                Name = NameUserUpdated,
                Email = EmailUserUpdated,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
}