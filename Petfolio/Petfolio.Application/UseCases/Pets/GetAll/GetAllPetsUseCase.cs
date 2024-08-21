using Petfolio.Communication.Responses;

namespace Petfolio.Application.UseCases.Pets.GetAll;
public class GetAllPetsUseCase
{
    public ResponseAllPetJson Execute()
    {
        return new ResponseAllPetJson
        {
            Pets = new List<ResponseShortPetJson>
            {
                new ResponseShortPetJson {
                    Id = 1,
                    Name = "Lucimara Barracuda",
                    Type = Communication.Enums.PetType.Dog,
                },
                new ResponseShortPetJson {
                    Id = 2,
                    Name = "Luana Piovana",
                    Type = Communication.Enums.PetType.Dog,
                },
                new ResponseShortPetJson {
                    Id = 3,
                    Name = "Minino Cururido",
                    Type = Communication.Enums.PetType.Cat,
                },
                new ResponseShortPetJson {
                    Id = 4,
                    Name = "Gato Bela",
                    Type = Communication.Enums.PetType.Cat,
                },
                new ResponseShortPetJson {
                    Id = 5,
                    Name = "Butizinha Codirosa",
                    Type = Communication.Enums.PetType.Ferret,
                }
            }
        };
    }
}
