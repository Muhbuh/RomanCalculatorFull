import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-roman-numerals-input-field',
  templateUrl: './roman-numerals-input-field.component.html',
  styleUrls: ['./roman-numerals-input-field.component.css'],
})
export class RomanNumeralsInputFieldComponent {
  @Input('label') LabelText = 'PlaceHolder Label';

    private _value: string = "";
  @Input()
  get Value(): string { return this._value; }
  set Value(Value: string) {
    this._value = (Value && Value.trim()) || '';
  }

  @Input('readOnly') ReadOnly = false;
}
