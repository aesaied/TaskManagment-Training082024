$(function () {

    //alert('file changed!');
    $('#tblEmployees').dataTable({
        "ajax": { 'url': jsonDataUrl, 'method': 'post' },
        "processing": true,
        "serverSide": true,

        "columnDefs": [{
            "targets": [0],
            "visible": true,
            "searchable": false
        }],
        lengthMenu:[2,10,20,50]
        ,

        "columns": [

            { "data": "id", "name": "id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "jobTitle", "name": "jobTitle", "autoWidth": true },
            { "data": "email", "name": "email", "autoWidth": true },
            { "data": "assignDate", "name": "assignDate", "autoWidth": true },
            { "data": "isActive", "name": "isActive", "autoWidth": true, orderable:false },
            {
                "data": "phoneNumber", "name": "phoneNumber", "autoWidth": true,
               
                   
            },
            {
                "data": "id", name: "action", orderable: false, render: function (data) {
                    return `<button id="btnGroupDrop1" type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
    Actions
</button>
<ul class="dropdown-menu" aria-labelledby="btnGroupDrop1">
    <li><a class="dropdown-item" href="employees/edit/${data}">Edit</a></li>
    <li><a class="dropdown-item" href="employees/view/${data}">View</a></li>
</ul>`;

                }
            }


        ]

    }
    );
}
);