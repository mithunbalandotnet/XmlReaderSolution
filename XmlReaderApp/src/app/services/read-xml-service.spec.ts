import { TestBed } from '@angular/core/testing';

import { ReadXmlService } from './read-xml-service';

describe('ReadXmlService', () => {
  let service: ReadXmlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReadXmlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
