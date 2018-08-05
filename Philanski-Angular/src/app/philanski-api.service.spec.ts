import { TestBed, inject } from '@angular/core/testing';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { PhilanskiApiService } from './philanski-api.service';

describe('PhilanskiApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports : [HttpClientModule],
      providers: [PhilanskiApiService]
    });
  });

  it('should be created', inject([PhilanskiApiService], (service: PhilanskiApiService) => {
    expect(service).toBeTruthy();
  }));
});
