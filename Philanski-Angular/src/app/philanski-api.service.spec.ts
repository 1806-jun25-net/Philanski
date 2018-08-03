import { TestBed, inject } from '@angular/core/testing';

import { PhilanskiApiService } from './philanski-api.service';

describe('PhilanskiApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PhilanskiApiService]
    });
  });

  it('should be created', inject([PhilanskiApiService], (service: PhilanskiApiService) => {
    expect(service).toBeTruthy();
  }));
});
