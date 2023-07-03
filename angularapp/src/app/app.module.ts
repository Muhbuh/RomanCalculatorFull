import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.main';
import { RomanNumeralsCalculatorComponent } from './components/roman-numerals-calculator/roman-numerals-calculator.component';
import { RomanNumeralsInputFieldComponent } from './components/roman-numerals-input-field/roman-numerals-input-field.component';
import { RomanNumeralsDropdownlistComponent } from './components/roman-numerals-dropdownlist/roman-numerals-dropdownlist.component';

@NgModule({
  declarations: [
    AppComponent,
    RomanNumeralsCalculatorComponent,
    RomanNumeralsInputFieldComponent,
    RomanNumeralsDropdownlistComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
