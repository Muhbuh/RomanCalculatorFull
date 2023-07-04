import { Component, Input } from '@angular/core';
import { IDropDownData } from '../roman-numerals-dropdownlist/dropdowndata.interface';
import { HttpClient } from '@angular/common/http';
import { BaseRouteReuseStrategy } from '@angular/router';

@Component({
  selector: 'app-roman-numerals-calculator',
  templateUrl: './roman-numerals-calculator.component.html',
  styleUrls: ['./roman-numerals-calculator.component.css'],
})
export class RomanNumeralsCalculatorComponent {
  @Input('floor') FloorValue = 'I';
  @Input('ceiling') CeilingValue = 'MMMCMXCIX';



  @Input('f1Label') Field1Label = "1st Element:";
  @Input('f1Value') Field1Value = "";

  @Input('f2Label') Field2Label = "2st Element:"
  @Input('f2Value') Field2Value = "";

    ;
  @Input('f3Label') Field3Label = "Result:";
  @Input('f3ReadOnly') Field3ReadOnly = true;
  @Input('f3Value') Field3Value = "Test";

  /*  @Input('ddata') DDData: Array<IDropDownData> =[{ id: 1, text: "Roman" }, { id: 2, text: "Arabic" }];*/
  public DDData: Array<IDropDownData> = [];
  private HTTP: HttpClient;

  constructor(http: HttpClient) {
    this.HTTP = http;
    http.get<Array<IDropDownData>>('/romancalculator/GetDropDownData').subscribe({
      next: result => this.DDData = result,
      error: error => console.log(error),
    })
  }

  public onSum() {
    this.HTTP.get<string>('/romancalculator/GetSum', { params: { summand1: this.Field1Value, summand2: this.Field2Value} }).subscribe({
      next: result => this.Field3Value = result,
      error: error => console.log(error),
    })
  }

  getField1Value(newItem: string) {
    this.Field1Value = newItem;
  }

  getField2Value(newItem: string) {
    this.Field2Value = newItem;
  }
}
