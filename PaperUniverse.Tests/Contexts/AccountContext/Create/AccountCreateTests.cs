using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;
using PaperUniverse.Tests.Contexts.AccountContext.Create.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.Create
{
    [TestClass]
    public class AccountCreateTests
    {
        private readonly MockRepository _repository;
        private readonly MockService _service;

        public AccountCreateTests()
        {
            _repository = new MockRepository();
            _service = new MockService();
        }

        [TestMethod]
        public async Task ShouldReturnFalseWhenUserEmailAlreadyExists()
        {
            var request = new Request 
            {
                Name = "Davi Francisco",
                Email = "email@email.com",
                Password = "123456"
            };
            var handler = new Handler(_repository, _service);
            var result = await handler.Handle(request, new CancellationToken());

            Assert.IsFalse(result.Success);
            Assert.AreEqual("O e-mail já está cadastrado no banco de dados.", result.Message);
        }

        [TestMethod]
        public async Task ShouldReturnTrueWhenUserIsValid()
        {
            var req = new Request
            {
                Name = "Davi Francisco",
                Email = "davi@gmail.com",
                Password = "123456"
            };
            var handler = new Handler(_repository, _service);
            var result = await handler.Handle(req, new CancellationToken());

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Status, 201);
            Assert.AreEqual("Cadastro realizado com sucesso.", result.Message);
        }

        [TestMethod]
        [DataRow("Da")]
        [DataRow("Davi Francisco Rodrigues dos Santos Pereira Ferraz de Vasconcelos")]
        public async Task ShouldReturnFalseWhenUserNameIsInvalid(string name)
        {
            var req = new Request
            {
                Name = name,
                Email = "davi@gmail.com",
                Password = "123456"
            };
            var handler = new Handler(_repository, _service);
            var result = await handler.Handle(req, new CancellationToken());

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Não foi possível realizar o seu cadastro.", result.Message);
        }

        [TestMethod]
        [DataRow("12345")]
        [DataRow("1234567891011121315486")]
        public async Task ShouldReturnFalseWhenUserPasswordIsInvalid(string password)
        {
            var req = new Request
            {
                Name = "Davi Francisco",
                Email = "davi@gmail.com",
                Password = password
            };
            var handler = new Handler(_repository, _service);
            var result = await handler.Handle(req, new CancellationToken());

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Não foi possível realizar o seu cadastro.", result.Message);
        }
    }
}