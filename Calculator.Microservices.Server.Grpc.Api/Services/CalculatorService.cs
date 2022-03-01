using Grpc.Core;

namespace Calculator.Microservices.Server.Grpc.Api
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        public override Task<ResultResponse> Add(AddRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = request.A + request.B });
        }

        public override Task<ResultResponse> Subtract(SubtractRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = request.A - request.B });
        }

        public override Task<ResultResponse> Multiply(MultiplyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = request.A * request.B });
        }

        public override Task<ResultResponse> Divide(DivideRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = request.A / request.B });
        }

        public override Task<ResultResponse> Pow(PowRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = Math.Pow(request.X, request.Y) });
        }

        public override Task<ResultResponse> Sqrt(SqrtRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultResponse { Result = Math.Sqrt(request.D) });
        }
    }
}
