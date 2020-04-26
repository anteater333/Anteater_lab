using NUnit.Framework;
using Prime.Service;

namespace Prime.UnitTests.Services
{
    [TestFixture]   // 단위 테스트가 포함된 클래스를 나타내는 특성
    public class PrimeService_IsPrimeShould
    {
        private PrimeService _primeService;

        [SetUp]
        public void Setup()
        {
            _primeService = new PrimeService();
        }

        [Test]  // 테스트 메서드임을 나타내는 특성
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var result = _primeService.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]   // 같은 코드를 실행하지만 서로 다른 입력 인수를 가지는 테스트 모음
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = _primeService.IsPrime(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}