import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiresAddComponent } from './tires-add.component';

describe('TiresAddComponent', () => {
  let component: TiresAddComponent;
  let fixture: ComponentFixture<TiresAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TiresAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TiresAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
