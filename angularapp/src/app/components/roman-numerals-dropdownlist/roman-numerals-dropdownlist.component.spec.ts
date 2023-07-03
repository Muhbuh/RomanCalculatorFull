import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RomanNumeralsDropdownlistComponent } from './roman-numerals-dropdownlist.component';

describe('RomanNumeralsDropdownlistComponent', () => {
  let component: RomanNumeralsDropdownlistComponent;
  let fixture: ComponentFixture<RomanNumeralsDropdownlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RomanNumeralsDropdownlistComponent]
    });
    fixture = TestBed.createComponent(RomanNumeralsDropdownlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
