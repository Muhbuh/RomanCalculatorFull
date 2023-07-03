import { Component, Input } from '@angular/core';
import { IDropDownData } from './dropdowndata.interface';

@Component({
  selector: 'app-roman-numerals-dropdownlist',
  templateUrl: './roman-numerals-dropdownlist.component.html',
  styleUrls: ['./roman-numerals-dropdownlist.component.css']
})
export class RomanNumeralsDropdownlistComponent {

  @Input('label') LabelText = 'Number Display';

  private _data: Array<IDropDownData> = [{ id: 1, text: "Roman" }, { id: 2, text: "Arabic" }];
  @Input()
  get Data(): Array<IDropDownData> { return this._data; }
  set Data(Value: Array<IDropDownData>) {
    this._data = Value;
  }
}
