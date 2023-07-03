import { Component, Input } from '@angular/core';
import { IDropDownData } from '../roman-numerals-dropdownlist/dropdowndata.interface';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-roman-numerals-calculator',
  templateUrl: './roman-numerals-calculator.component.html',
  styleUrls: ['./roman-numerals-calculator.component.css'],
})
export class RomanNumeralsCalculatorComponent {
  @Input('floor') FloorValue = 'I';
  @Input('ceiling') CeilingValue = 'MMMCMXCIX';



  @Input('f1Label') Field1Label = "1st Element:";
  @Input('f2Label') Field2Label = "2st Element:";
  @Input('f3Label') Field3Label = "Result:";

  @Input('f3ReadOnly') Field3ReadOnly = true;

  /*  @Input('ddata') DDData: Array<IDropDownData> =[{ id: 1, text: "Roman" }, { id: 2, text: "Arabic" }];*/
  public DDData: Array<IDropDownData> = [];

  constructor(http: HttpClient) {
    http.get<Array<IDropDownData>>('/RomanCalculator/GetDropDownData').subscribe(result => {
      this.DDData = result;
    }, error => console.error(error));
  }
}
