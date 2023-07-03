import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RomanNumeralsCalculatorComponent } from './roman-numerals-calculator.component';

describe('RomanNumeralsCalculatorComponent', () => {
  let component: RomanNumeralsCalculatorComponent;
  let fixture: ComponentFixture<RomanNumeralsCalculatorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RomanNumeralsCalculatorComponent]
    });
    fixture = TestBed.createComponent(RomanNumeralsCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
