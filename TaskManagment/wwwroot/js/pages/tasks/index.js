$(function () {

    //alert('file changed!');
    $('#tblTasks').dataTable({
        "ajax": { 'url': taskJsonUrl, 'method': 'post' },
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
            { "data": "title", "name": "title", "autoWidth": true },
            { "data": "projectName", "name": "project.name", "autoWidth": true },
            { "data": "createdDate", "name": "createdDate", "autoWidth": true },
            { "data": "dueDate", "name": "dueDate", "autoWidth": true },
            { "data": "hasAttachment", "name": "hasAttachment", "autoWidth": true, orderable:false },
            {
                "data": "currentStatus", "name": "currentStatus", "autoWidth": true,
                render: function (data, type, row, meta) {


                    /* New=1,
        InProgress,
        Done,
        Canceled */

                    let statusText = 'New';
                    let statusClass = 'bg-primary';
                    switch (data) {
                        case '2':
                            statusText = 'In progress';
                            statusClass = 'bg-info';
                            break;
                        case '3':
                            statusText = 'Done';
                            statusClass = 'bg-success';
                            break;
                        case '4':
                            statusText = 'Canceled';
                            statusClass = 'bg-danger';
                            break;

                    }

                    return `<span class="badge ${statusClass}">${statusText}</span>`;
                }
            },
            {
                "data": "id", name: "action", orderable: false, render: function (data) {
                    return `<button id="btnGroupDrop1" type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
    Actions
</button>
<ul class="dropdown-menu" aria-labelledby="btnGroupDrop1">
    <li><a class="dropdown-item" href="tasks/edit/${data}">Edit</a></li>
    <li><a class="dropdown-item" href="tasks/view/${data}">View</a></li>
</ul>`;

                }
            }


        ]

    }
    );
}
);