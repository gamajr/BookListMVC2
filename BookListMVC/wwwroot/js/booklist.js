var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/tblbooks/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "30%" }, //These fields MUST be in CamelCase!!!
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text -center">
                        <a href="/tblbooks/upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;'
                            onClick=Delete('/tblbooks/delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "30%"

            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"

    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();

                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });

}

