import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MailContent } from './mail-content';

describe('MailContent', () => {
  let component: MailContent;
  let fixture: ComponentFixture<MailContent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MailContent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MailContent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
