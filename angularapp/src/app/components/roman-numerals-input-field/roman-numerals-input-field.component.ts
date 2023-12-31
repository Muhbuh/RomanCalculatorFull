import { Component, Input } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-roman-numerals-input-field',
  templateUrl: './roman-numerals-input-field.component.html',
  styleUrls: ['./roman-numerals-input-field.component.css'],
})
export class RomanNumeralsInputFieldComponent {
  @Input('invalid') InvalidValue = false;
  @Input('error') ErrorMessage = "Input is not a valid number";

  @Input('label') LabelText = 'PlaceHolder Label';

    private _value: string = "";
  @Input()
  get Value(): string { return this._value; }
  set Value(Value: string) {
    this._value = (Value && Value.trim()) || '';
  }

  @Input('readOnly') ReadOnly = false;

  @Output() newValueEvent = new EventEmitter<string>();
  addNewValue(value: string) {
    this.newValueEvent.emit(value);
  }
}
