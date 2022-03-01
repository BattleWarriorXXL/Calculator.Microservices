import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CalculatorClient } from './CalculatorServiceClientPb';
import {
    AddRequest,
    SubtractRequest,
    MultiplyRequest,
    DivideRequest,
    PowRequest,
    SqrtRequest
} from './calculator_pb';

@Injectable({
    providedIn: 'root',
})
export class CalculatorService {
    private service: CalculatorClient;

    constructor() {
        this.service = new CalculatorClient(environment.gRpcEndpoint, null, null);
    }

    public async add(a: number, b: number) : Promise<number> {
        const request: AddRequest = new AddRequest();
        request.setA(a);
        request.setB(b);

        const call = await this.service.add(request, null);
        return call.getResult();
    }

    public async subtract(a: number, b: number) : Promise<number> {
        const request: SubtractRequest = new SubtractRequest();
        request.setA(a);
        request.setB(b);

        const call = await this.service.subtract(request, null);
        return call.getResult();
    }

    public async multiply(a: number, b: number) : Promise<number> {
        const request: MultiplyRequest = new MultiplyRequest();
        request.setA(a);
        request.setB(b);

        const call = await this.service.multiply(request, null);
        return call.getResult();
    }

    public async divide(a: number, b: number) : Promise<number> {
        const request: DivideRequest = new DivideRequest();
        request.setA(a);
        request.setB(b);

        const call = await this.service.divide(request, null);
        return call.getResult();
    }

    public async pow(x: number, y: number) : Promise<number> {
        const request: PowRequest = new PowRequest();
        request.setX(x);
        request.setY(y);

        const call = await this.service.pow(request, null);
        return call.getResult();
    }

    public async sqrt(d: number) : Promise<number> {
        const request: SqrtRequest = new SqrtRequest();
        request.setD(d);

        const call = await this.service.sqrt(request, null);
        return call.getResult();
    }
}