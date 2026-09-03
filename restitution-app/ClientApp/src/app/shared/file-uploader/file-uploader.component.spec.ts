import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule, UntypedFormArray } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ControlContainer } from '@angular/forms';

import { FileUploaderComponent } from './file-uploader.component';

describe('FileUploaderComponent', () => {
  let component: FileUploaderComponent;
  let fixture: ComponentFixture<FileUploaderComponent>;
  let snackBar: { open: ReturnType<typeof vi.fn> };

  beforeEach(async () => {
    const formBuilder = new FormBuilder();
    const form = formBuilder.group({
      totalAttachmentSize: [0],
      details: formBuilder.group({})
    });

    snackBar = { open: vi.fn() };

    await TestBed.configureTestingModule({
      declarations: [FileUploaderComponent],
      imports: [ReactiveFormsModule],
      providers: [
        { provide: ControlContainer, useValue: { control: form.get('details') } },
        { provide: MatSnackBar, useValue: snackBar }
      ]
    })
      .overrideComponent(FileUploaderComponent, { set: { template: '' } })
      .compileComponents();
    fixture = TestBed.createComponent(FileUploaderComponent);
    component = fixture.componentInstance;
    component.documents = formBuilder.array([]) as UntypedFormArray;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('rejects unsupported file types without adding a document', () => {
    component.onFilesAdded(fileList(new File(['not an image'], 'malware.exe')));

    expect(component.documents.length).toBe(0);
    expect(snackBar.open).toHaveBeenCalledWith('Unsupported file type', 'Fail', expect.any(Object));
  });

  it('rejects files larger than 2MB without adding a document', () => {
    component.onFilesAdded(fileList(new File([new ArrayBuffer(component.MAX_FILE_SIZE + 1)], 'large.pdf')));

    expect(component.documents.length).toBe(0);
    expect(snackBar.open).toHaveBeenCalledWith('File cannot exceed 2MB', 'Fail', expect.any(Object));
  });
});

function fileList(file: File): FileList {
  return {
    0: file,
    length: 1,
    item: (index: number) => (index === 0 ? file : null)
  } as unknown as FileList;
}
