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

  // Replace later with api call to get config
  FloorValues = ["I", "1"];
  CeilingValues = ["MMMCMXCIX", 3999];

  NumberType = 0;

  @Input('floor') FloorValue = this.FloorValues[0];
  @Input('ceiling') CeilingValue = this.CeilingValues[0];



  @Input('f1Label') Field1Label = "1st Element:";
  @Input('f1Value') Field1Value = "";
  @Input('f1invalid') Field1InvalidValue = true;
  @Input('f1errormessage') Field1ErrorMesage = "";

  @Input('f2Label') Field2Label = "2st Element:"
  @Input('f2Value') Field2Value = "";
  @Input('f2invalid') Field2InvalidValue = true;
  @Input('f2errormessage') Field2ErrorMesage = "";

  ;
  @Input('f3Label') Field3Label = "Result:";
  @Input('f3ReadOnly') Field3ReadOnly = true;
  @Input('f3Value') Field3Value = "";
  @Input('f3invalid') Field3InvalidValue = false;
  @Input('f3errormessage') Field3ErrorMesage = "";

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

  // Need to check later why the return JSON has to be converted and reconverted agaig
  public onSum() {
    if (!this.Field1InvalidValue && !this.Field2InvalidValue) {
      this.HTTP.get<string>('/romancalculator/GetSum', { params: { summand1: this.Field1Value, summand2: this.Field2Value, numberType: this.NumberType } }).subscribe({
        next: result => {
          var str = JSON.stringify(result);
          var data = JSON.parse(str);
          if (data.succes) {
            this.Field3Value = data.text
            this.Field3ErrorMesage = "";
            this.Field3InvalidValue = false;
          }
          else {
            console.log("Not implemented");
          }
        },
        error: error => console.log(error),
      })
    }
    else {
      this.Field3InvalidValue = true;
      this.Field3ErrorMesage = "Inputs are not valid numbers";
    }
  }



  getField1Value(newItem: string) {
    this.Field1Value = newItem;

    this.HTTP.get<string>('/romancalculator/CheckNum', { params: { number: this.Field1Value, numberType: this.NumberType } }).subscribe({
      next: result => {
        var str = JSON.stringify(result);
        var data = JSON.parse(str);
        if (data.succes) {
          this.Field1InvalidValue = false;
        }
        else {
          this.Field1InvalidValue = true;
          this.Field1ErrorMesage = data.text;
        }
      },
      error: error => console.log(error),
    })
  }

  getField2Value(newItem: string) {
    this.Field2Value = newItem;

    this.HTTP.get<string>('/romancalculator/CheckNum', { params: { number: this.Field2Value, numberType: this.NumberType } }).subscribe({
      next: result => {
        var str = JSON.stringify(result);
        var data = JSON.parse(str);
        if (data.succes) {
          this.Field2InvalidValue = false;
        }
        else {
          this.Field2InvalidValue = true;
          this.Field2ErrorMesage = data.text;
        }
      },
      error: error => console.log(error),
    })
  }

  getDDDataValue(newItem: string) {
    console.log(newItem);
    var option = Number(newItem);
    console.log(option);

    if (isNaN(option)) {
      console.log("Drop down list returned non numeric value"); // Replace with proper error message
      return;
    }

    this.NumberType = option;

    this.FloorValue = this.FloorValues[option];
    this.CeilingValue = this.CeilingValues[option];
  }
}
