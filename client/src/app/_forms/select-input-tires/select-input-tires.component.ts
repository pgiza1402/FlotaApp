import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-select-input-tires',
  templateUrl: './select-input-tires.component.html',
  styleUrls: ['./select-input-tires.component.scss']
})
export class SelectInputTiresComponent implements ControlValueAccessor {
  @Input() cars : string[]
  @Input() label : string;
  @Input() disabled: boolean = false


  constructor(@Self() @Optional() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  
 writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
  
  }
  registerOnTouched(fn: any): void {
    
  }
}