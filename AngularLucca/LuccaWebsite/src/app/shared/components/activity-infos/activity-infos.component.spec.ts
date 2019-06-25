import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityInfosComponent } from './activity-infos.component';

describe('ActivityInfosComponent', () => {
  let component: ActivityInfosComponent;
  let fixture: ComponentFixture<ActivityInfosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivityInfosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityInfosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
