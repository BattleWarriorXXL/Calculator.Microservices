import { Component } from '@angular/core';
import { OperationTypes } from '../helpers/OperationTypes';
import { CalculatorService } from '../services/CalculatorService';

@Component({
    selector: 'app-calculator',
    templateUrl: './calculator.component.html'
})
export class CalculatorComponent {
    public a: number = 0;
    public b: number = 0;
    public x: number = 0;
    public y: number = 0;
    public d: number = 0;
    public result: number = 0;
    public selectedOperationType: OperationTypes = OperationTypes.ADD;

    constructor(private calculatorService: CalculatorService) {}

    public async execute() {
        switch (this.selectedOperationType) {
            case OperationTypes.ADD:
                this.result = await this.calculatorService.add(this.a, this.b);
                break;
            case OperationTypes.SUBTRACT:
                this.result = await this.calculatorService.subtract(this.a, this.b);
                break;
            case OperationTypes.MULTIPLY:
                this.result = await this.calculatorService.multiply(this.a, this.b);
                break;
            case OperationTypes.DIVIDE:
                this.result = await this.calculatorService.divide(this.a, this.b);
                break;
            default:
                throw "Not selected operator";
        }
    }

    public async executePow() {
        const result = await this.calculatorService.pow(this.x, this.y);
        this.result = result;
    }

    public async executeSqrt() {
        const result = await this.calculatorService.sqrt(this.d);
        this.result = result;
    }

    public onOperationSelected(operationType: OperationTypes) {
        this.selectedOperationType = operationType;
        console.log(this.selectedOperationType);
        
    }
}