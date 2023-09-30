﻿
var dataTable;
$(document).ready(function () {
    loadDataTable();
}
);

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll" 
        },
        "columns": [
            { "data": "title", "width": "10%" },
            { "data": "author", "width": "10%" },
            { "data": "isbn", "width": "10%" },
            { "data": "listPrice", "width": "10%" },
            { "data": "price50", "width": "10%" },
            { "data": "price100", "width": "10%" },
            { "data": "category.categoryName", "width": "10%" },

             {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="w-75 btn-group" role="group">
                  <a href="/Admin/Product/Upsert?id=${data}"   class="btn btn-primary mx-2" >
                      <i class="bi bi-pencil-square"></i>Edit</a> 
                  <a   onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger mx-2" >
                     <i class="bi bi-trash"></i>Delete</a>
                      </div>

                            `
                },
                "width": "20%"
            }
        
        ]
    }

    );
};
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
};
