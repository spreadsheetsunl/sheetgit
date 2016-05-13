/***********************
 *  CUSTOM TEMPLATES   *
 ***********************/
//#5353ac", "#008fb5", "#f1c109"

var kelly_colors = [
    "#FFB300", // Vivid Yellow
    "#803E75", // Strong Purple
    "#FF6800", // Vivid Orange
    "#A6BDD7", // Very Light Blue
    "#C10020", // Vivid Red
    "#CEA262", // Grayish Yellow
    "#817066", // Medium Gray

    // The following don't work well for people with defective color vision
    "#007D34", // Vivid Green
    "#F6768E", // Strong Purplish Pink
    "#00538A", // Strong Blue
    "#FF7A5C", // Strong Yellowish Pink
    "#53377A", // Strong Violet
    "#FF8E00", // Vivid Orange Yellow
    "#B32851", // Strong Purplish Red
    "#F4C800", // Vivid Greenish Yellow
    "#7F180D", // Strong Reddish Brown
    "#93AA00", // Vivid Yellowish Green
    "#593315", // Deep Yellowish Brown
    "#F13A13", // Vivid Reddish Orange
    "#232C16", // Dark Olive Green
]

var last_kelly = 0;

var myTemplateConfig = {
    colors: ["#676767", "#D43A3A", "#3A62D4"],
    branch: {
        lineWidth: 5,
        spacingX: 16
    },
    commit: {
        spacingY: -30,
        dot: {
            size: 6
        },
        message: {
            display: false,
            displayAuthor: false,
            displayBranch: false,
            displayHash: false,
            font: "normal 12pt Calibri",
            color: "black"
        },
        tooltipHTMLFormatter: function (commit) {
            return "" + commit.message;
        }
    }
};
var myTemplate = new GitGraph.Template(myTemplateConfig);

var branches = new Object();

/***********************
 *    INITIALIZATION   *
 ***********************/

var config = {
    template: "metro"       // could be: "blackarrow" or "metro" or `myTemplate` (custom Template object)
    //, mode: "compact"     // special compact mode : hide messages & compact graph
};

branches["master"] = [
    new GitGraph({
    //mode: "compact",
    template: "metro"
}),null];

var selectedCommit;

branches["master"][0].template = myTemplate;

/***********************
 *       EVENTS        *
 ***********************/

gitGraph.canvas.addEventListener("commit:mouseover", function (event) {
    console.log("You're over a commit.", "Here is a bunch of data ->", event.data);
});

// Attach a handler to the commit

function comTouch(commit) {
    console.log("You just clicked my commit.", commit);
    commit.dotColor = "white";
    commit.dotStrokeWidth = 10;

    if (selectedCommit != null && commit != selectedCommit) {
        selectedCommit.dotColor = selectedCommit.dotStrokeColor;
        selectedCommit.dotStrokeWidth = null;
        selectedCommit.parent.render();

        if (commit.changes != null) {
            var x = -1;
            var y = -1;
            var pathA = "";
            var pathB = "";
            var commitA = selectedCommit;
            var commitB = commit;
            var final;

            console.log("new" + commit.changes);
            console.log("old" + selectedCommit.changes);
            
            while (commitA != commitB) {
                if (commitA.y > commitB.y) {
                    pathA = pathA.concat("R|"+commitA.changes+"***");
                    commitA = commitA.parentCommit;
                } else {
                    pathB = pathB.concat("A|"+commitB.changes+"***");
                    commitB = commitB.parentCommit;
                }
            }
            final = pathA.concat(pathB);
            console.log(final);
            window.external.ExecuteMacro(final);
        }
    }
    selectedCommit = commit;

    commit.parent.render();
}

function refreshLog(json) {
    console.log(json);
    parsed = JSON.parse(json);
    var master = branches["master"][0];

    parsed.forEach(function(entry) {
        //<b>Commit message</b><br><i>Cell C28 = 15/09/2001</i>"
        if (branches[entry.author] == null) {
            branches[entry.author] = [master.branch(entry.author), last_kelly++];
        }

        gitGraph.commit({
            message: "<b>"+entry.message+"</b><br><i>"+entry.author+" <"+entry.email+"><br>"+entry.date+"</i>",
            onClick: function (commit) { comTouch(commit); },
            changes: "Initial",
            dotColor: "#D43A3A"
        });

        if (selectedCommit == null) {
            var com = master.commits[master.commits.length-1];
            comTouch(com);
        }
        selectedCommit.parent.render();
    });
    
}