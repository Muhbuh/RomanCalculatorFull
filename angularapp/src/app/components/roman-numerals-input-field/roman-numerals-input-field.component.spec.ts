import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RomanNumeralsInputFieldComponent } from './roman-numerals-input-field.component';

describe('RomanNumeralsInputFieldComponent', () => {
  let component: RomanNumeralsInputFieldComponent;
  let fixture: ComponentFixture<RomanNumeralsInputFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RomanNumeralsInputFieldComponent]
    });
    fixture = TestBed.createComponent(RomanNumeralsInputFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
