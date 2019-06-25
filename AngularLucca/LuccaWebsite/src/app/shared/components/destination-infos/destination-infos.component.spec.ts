import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationInfosComponent } from './destination-infos.component';

describe('DestinationInfosComponent', () => {
  let component: DestinationInfosComponent;
  let fixture: ComponentFixture<DestinationInfosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DestinationInfosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DestinationInfosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
