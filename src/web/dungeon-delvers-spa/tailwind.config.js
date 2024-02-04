/** @type {import('tailwindcss').Config} */
const plugin = require("tailwindcss/plugin");

module.exports = {
  content: ["./src/**/*.{html, ts}"],
  purge: ["./src/**/*.{html, ts}"],
  theme: {
    extend: {
      colors: {
        "dd-red": {
          DEFAULT: "#f87a96",
        },
        "dd-blue": {
          light: "#86daeb",
          DEFAULT: "#0090d6",
          dark: "#0057a8",
        },
        "dd-purple": {
          dark: "#110036",
          DEFAULT: "#110036",
        },
      },
    },
  },
  plugins: [
    plugin(
      function ({ matchUtilities, theme }) {
        matchUtilities(
          {
            "bg-radient": (value) => ({
              "background-image": `radial-gradient(${value}, var(--tw-gradient-stops))`,
            }),
          },
          {
            values: theme("radialGradients"),
          }
        );
      },
      {
        theme: {
          radialGradients: function () {
            const shapes = ["circle", "ellipse"];
            const pos = {
              c: "center",
              t: "top",
              b: "bottom",
              l: "left",
              r: "right",
              tl: "top left",
              tr: "top right",
              bl: "bottom left",
              br: "bottom right",
            };
            let result = {};
            for (const shape of shapes)
              for (const [posName, posValue] of Object.entries(pos))
                result[`${shape}-${posName}`] = `${shape} at ${posValue}`;
            return result;
          },
        },
      }
    ),
  ],
};
